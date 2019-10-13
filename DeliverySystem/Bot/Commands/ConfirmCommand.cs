using System;
using System.ComponentModel.Design;
using System.Linq;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DeliverySystem.Bot.Commands
{
    public class ConfirmCommand : Command
    {
        public override string Name => "/confirmdelivery";


        public override void Execute(Message message, TelegramBotClient client, ApiDbContext dbContext) // Confirm current order and gives next
        {
            var courier = dbContext.Couriers.FirstOrDefault(c => c.TelegramUsrName == message.From.Username);
            var order = dbContext.ActiveCourierDeliveries.FirstOrDefault(c =>
                c.Courier.Id == courier.Id);

            order.DeliveryOrder.Status = 3;
            dbContext.Remove(order);
            //confirmed

            var newOrder = dbContext.ActiveCourierDeliveries.FirstOrDefault(c =>
                c.Courier.Id == courier.Id);

            if (newOrder == null)// check end of work
            {
                client.SendTextMessageAsync(courier.TelegramChatId, "Work is finished to start new work type /work");
                courier.Status = 1;
                
                return;
            }

            dbContext.SaveChanges();
            
            client.SendTextMessageAsync(courier.TelegramChatId,
                $"Delivery :{order.DeliveryOrderId} </br>" +
                $"<a src='http://www.google.com/maps/place/{order.DeliveryOrder.Latitude},{order.DeliveryOrder.Longitude}'>Your destination<a> ",
                ParseMode.Html);
            //send courier next order
        }
    }
}