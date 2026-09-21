using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.Locations
{
    public class Bank
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public bool HasBankCard { get; set; }
        public bool HasAccount { get; set; }

        public Bank(string name, int money)
        {
            Name = name;
            Money = money;
            HasBankCard = false;
            HasAccount = false;
        }

        public void CreateAccount()
        {
            if (!HasAccount)
            {
                HasAccount = true;
                Console.WriteLine("Account created successfully.");
            }
            else
            {
                Console.WriteLine("You already have an account.");
            }
        }

        public void CheckMoney()
        {
            if (HasAccount)
            {
                Console.WriteLine($"Your account balance is: {Money}");
            }
            else
            {
                Console.WriteLine("You need to create an account first.");
            }
        }

        public void DepositMoney(int amount)
        {
            if (HasAccount)
            {
                Money += amount;
                Console.WriteLine($"Deposited {amount}. New balance: {Money}");
            }
            else
            {
                Console.WriteLine("You need to create an account first.");
            }
        }

        public void WithdrawMoney(int amount)
        {
            if (HasAccount)
            {
                if (amount <= Money)
                {
                    Money -= amount;
                    Console.WriteLine($"Withdrew {amount}. New balance: {Money}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("You need to create an account first.");
            }
        }

        public void GetBankCard()
        {
            if (HasAccount)
            {
                HasBankCard = true;
                Console.WriteLine("Bank card issued successfully.");
            }
            else
            {
                Console.WriteLine("You need to create an account first.");
            }
        }

        public void PayBill(int amount)
        {
            if (HasAccount && HasBankCard)
            {
                if (amount <= Money)
                {
                    Money -= amount;
                    Console.WriteLine($"Paid bill of {amount}. New balance: {Money}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds to pay the bill.");
                }
            }
            else
            {
                Console.WriteLine("You need to have an account and a bank card to pay bills.");
            }
        }

        public void ShowBankCard()
        {
            if (HasBankCard)
            {
                Console.WriteLine("You have a bank card.");
            }
            else
            {
                Console.WriteLine("You do not have a bank card.");
            }
        }

        public void ShowAccount()
        {
            if (HasAccount)
            {
                Console.WriteLine("You're account is active.");
                Console.WriteLine("AccountNumber: 123456789");
            }
            else
            {
                Console.WriteLine("You do not have an account.");
            }
        }

        public void CloseAccount()
        {
            if (HasAccount)
            {
                HasAccount = false;
                HasBankCard = false;
                Console.WriteLine("Account closed successfully.");
            }
            else
            {
                Console.WriteLine("You do not have an account to close.");
            }
        }
    }
}
