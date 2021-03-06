﻿using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.Views
{
    public class MainMenu : ConsoleMenu
    {
        private static APIService user = new APIService();


        public MainMenu()
        { 
            AddOption("View your current balance", ViewBalance)
                .AddOption("View your past transfers", ViewTransfers)
                .AddOption("View Transfer Details", ViewRequests)
                .AddOption("Send TE bucks", SendTEBucks)
                .AddOption("Request TE bucks", RequestTEBucks)
                .AddOption("Log in as different user", Logout)
                .AddOption("Exit", Exit);
        }

        protected override void OnBeforeShow()
        {
            Console.WriteLine($"TE Account Menu for User: {UserService.GetUserName()}");
        }

        private MenuOptionResult ViewBalance()
        {
            //TODO
            
            Console.WriteLine($"Your current balance is: {user.GetUserAccountBalance(UserService.GetUserId()).Balance:C}");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfers()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Transfer Id     From         To            Amount");
            Console.WriteLine("--------------------------------------------------");
            List<Transfer> transferList = user.ViewTransfers(UserService.GetUserId());
            foreach (Transfer transfer in transferList)
            {
                Console.WriteLine($"{transfer.TransferID}    {transfer.AccountFromName} {transfer.AccountToName} {transfer.Amount}");
            }

            Console.WriteLine("Please hit enter to return to main menu");
            return MenuOptionResult.WaitAfterMenuSelection;
        }


        private MenuOptionResult ViewRequests()
        {
            Console.Write("Please Enter Transfer ID: ");
            int transferId = int.Parse(Console.ReadLine());

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Transfer Details");
            Console.WriteLine("--------------------------------------------------");

            
            //List<Transfer> transferDetails = user.ViewTransfersDetails(transferId);
            
            
            Transfer details = user.ViewTransfersDetails(transferId);
            Console.WriteLine($"ID: {details.TransferID}");
            Console.WriteLine($"From: {details.AccountFromName}");
            Console.WriteLine($"To: {details.AccountToName}");
            Console.WriteLine($"Type: {details.TypeName}");
            Console.WriteLine($"Status: {details.StatusName}");
            Console.WriteLine($"Amount: {details.Amount}");
            Console.WriteLine("Please hit enter to return to main menu");

            return MenuOptionResult.WaitAfterMenuSelection;


            //Console.WriteLine("---------------------------------------------------");
            //Console.Write("Select Transfer ID (0 to cancel): ");
            //int recipientId = int.Parse(Console.ReadLine());
            //if (recipientId == 0)
            //{
            //    Console.WriteLine("Please hit enter to return to main menu");
            //    return MenuOptionResult.WaitAfterMenuSelection;

            //}


            //Console.WriteLine("Not yet implemented!");
            //return MenuOptionResult.WaitAfterMenuSelection;
        }


        private MenuOptionResult SendTEBucks()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Users Id     Name");
            Console.WriteLine("--------------------------------------------------");
            List<User> userList = user.GetAllUsers();
            foreach (User user in userList)
            {
                Console.WriteLine($"{user.UserId}      {user.Username}");
            }
            Console.WriteLine("---------------------------------------------------");
            Console.Write("Select Recipient ID (0 to cancel): ");
            int recipientId = int.Parse(Console.ReadLine());
            if (recipientId == 0)
            {
                Console.WriteLine("Please hit enter to return to main menu");
                return MenuOptionResult.WaitAfterMenuSelection;

            }
            else
            {
                Console.Write("Enter Amount: ");
                decimal transferAmount = decimal.Parse(Console.ReadLine());
                Transfer newTransfer = new Transfer();
                newTransfer.TransferTypeID = TransferType.Send;
                newTransfer.TransferStatusID = TransferStatus.Approved;
                newTransfer.AccountTo = recipientId;
                newTransfer.Amount = transferAmount;
                user.AddTransfer(newTransfer);
                Console.WriteLine("Please hit enter to return to main menu");
                return MenuOptionResult.WaitAfterMenuSelection;
            }
        
        }

        private MenuOptionResult RequestTEBucks()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult Logout()
        {
            UserService.SetLogin(new API_User()); //wipe out previous login info
            return MenuOptionResult.CloseMenuAfterSelection;
        }

    }
}
