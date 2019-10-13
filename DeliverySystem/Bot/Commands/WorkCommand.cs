using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DeliverySystem.Bot.Commands
{
    public class WorkCommand : Command
    {
        public override string Name => "/work";

        public override async void Execute(Message message, TelegramBotClient client, ApiDbContext dbContext)
        {
            var startWorkTime = Config.AppConfiguration.StartWorkTime;
            var endWorkTime = Config.AppConfiguration.EndWorkTime;


            // Check work time
            if (DateTime.Now.TimeOfDay > Config.AppConfiguration.EndWorkTime ||
                DateTime.Now.TimeOfDay < Config.AppConfiguration.StartWorkTime)
            {
                await client.SendTextMessageAsync(message.Chat.Id,
                    $"Sorry, u're not in time. U'r work time is {startWorkTime:HH-mm}- {endWorkTime:HH-mm}. Come back in time ");
                return;
            }

            await client.SendTextMessageAsync(message.Chat.Id, "Wait few seconds, i'm choosing the orders for u");

            var courier = dbContext.Couriers.FirstOrDefaultAsync(c => c.TelegramUsrName == message.From.Username)
                .Result;

            var allDeliveryOrders = dbContext.DeliveryOrders.Where(c =>
                c.Status==1 && c.DateTime.Date == DateTime.Today && c.StockId == courier.StockId);

            var deliveriesCount = (courier.Capacity < allDeliveryOrders.Count())
                ? courier.Capacity
                : allDeliveryOrders.Count();
            await client.SendTextMessageAsync(message.Chat.Id, deliveriesCount.ToString());

            var result = Enumerable.Range(0, deliveriesCount).Select(c => allDeliveryOrders.ElementAt(c)).ToList();

            var route = new List<DeliveryOrder>();
            // Find optimal route between stock and first delivery destination
            var minDistance = GetDistance(courier.Stock.Latitude, courier.Stock.Longitude,
                result[0].Latitude, result[0].Longitude);
            int minIndex = 0;
            for (int i = 1; i < result.Count; i++)
            {
                var distance = GetDistance(courier.Stock.Latitude, courier.Stock.Longitude,
                    result[i].Longitude, result[i].Longitude);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    minIndex = i;
                }
            }

            route.Add(result[minIndex]);
            result.RemoveAt(minIndex);

            // Find optimal routs between deliveries
            while (courier.Capacity != route.Count)
            {
                minDistance = GetDistance(route.Last().Latitude, route.Last().Longitude,
                    result[0].Latitude, result[0].Longitude);
                minIndex = 0;
                for (int i = 1; i < result.Count(); i++)
                {
                    var distance = GetDistance(route.Last().Latitude, route.Last().Longitude,
                        result[i].Latitude, result[i].Longitude);
                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        minIndex = i;
                    }
                }
                
                route.Add(result[minIndex]);
                result.RemoveAt(minIndex);
                
            }

            var activeDeliveries = new List<ActiveCourierDelivery>();

            await client.SendTextMessageAsync(message.Chat.Id, "Get packages");
            // Convert to active deliveries
            for (int i = 0; i < route.Count; i++)

            {
                activeDeliveries.Add(new ActiveCourierDelivery{Courier = courier,Id = i+1, CourierId = courier.Id, DeliveryOrder = route[i] , DeliveryOrderId = route[i].Id});
            }

            await dbContext.ActiveCourierDeliveries.AddRangeAsync(activeDeliveries);
            await dbContext.SaveChangesAsync();


        }
        
        public static async Task AcceptPackageReceiving(ApiDbContext dbContext, int courierId)
        {
            var courier = await dbContext.Couriers.FirstOrDefaultAsync(c => c.Id == courierId);
            var activeorder =
                await dbContext.ActiveCourierDeliveries.FirstOrDefaultAsync(c => c.CourierId == courierId);
            courier.Status = 3;

            await dbContext.SaveChangesAsync();

            var client = Bot.GetClient();

            await client.SendTextMessageAsync(courier.TelegramChatId,
                "U're receive packages. </br>" +$"Delivery:{activeorder.Id} </br>"+
                $"<a src='http://www.google.com/maps/place/{activeorder.DeliveryOrder.Latitude},{activeorder.DeliveryOrder.Longitude}'>Your destination<a> ",
                ParseMode.Html);
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) +
                             Math.Pow(y2 - y1, 2));
        }
    }
}