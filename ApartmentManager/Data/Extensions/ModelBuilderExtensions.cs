using Data.Entity;
using Data.Enum;
using Data.Relationships;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Account>().HasData(
                new Account() { ID = 1, Acc = "admin", Pass = "admin" });
            modelbuilder.Entity<Bill>().HasData(
                new Bill() { ID = 1, IDRTC = 1, ElectricQuantity = 150, Active = Active.Yes, PayDate = DateTime.Now, TotalMoney = 1000000 });
            modelbuilder.Entity<DepositsContract>().HasData(
                new DepositsContract { ID = 1, IDRoom = 1, IDLeader = 1, DepositsDate = DateTime.Now, ReceiveDate = DateTime.Now, CheckOutDate = DateTime.Now, Money = 10000 });
            modelbuilder.Entity<Furniture>().HasData(
                new Furniture { ID = 1, Name = "Chair" });
            modelbuilder.Entity<PaymentExtension>().HasData(
                new PaymentExtension { ID = 1, IDBill = 1, Days = DateTime.Now });
            modelbuilder.Entity<People>().HasData
                (new People() { ID = 1, Name = "Jonhny Deep", Birthday = DateTime.Now, Email = "thanh@gmail.com", PhoneNumber = "1234", IDCard = "1234123", Address = "Vietnam" },
                new People() { ID = 2, Name = "Emily Stone", Birthday = new DateTime(new Random().Next(1990, 2001), new Random().Next(1, 13), new Random().Next(1, 29)), Email = "emily@gmail.com", PhoneNumber = "5678", IDCard = "56781234", Address = "USA" },
                new People() { ID = 3, Name = "Robert Smith", Birthday = new DateTime(new Random().Next(1990, 2001), new Random().Next(1, 13), new Random().Next(1, 29)), Email = "robert@gmail.com", PhoneNumber = "91011", IDCard = "910111213", Address = "Canada" },
                new People() { ID = 4, Name = "Anna Johnson", Birthday = new DateTime(new Random().Next(1990, 2001), new Random().Next(1, 13), new Random().Next(1, 29)), Email = "anna@gmail.com", PhoneNumber = "1415", IDCard = "14151617", Address = "Australia" },
                new People() { ID = 5, Name = "Michael Brown", Birthday = new DateTime(new Random().Next(1990, 2001), new Random().Next(1, 13), new Random().Next(1, 29)), Email = "michael@gmail.com", PhoneNumber = "1819", IDCard = "18192021", Address = "UK" });
            modelbuilder.Entity<RentalContract>().HasData(
                new RentalContract { ID = 1, IDroom = 1, ReceiveDate = DateTime.Now, CheckOutDate = DateTime.Now, RoomMoney = 100, ElectricMoney = 100, WaterMoney = 100, ServiceMoney = 100, Active = Active.Yes },
                new RentalContract { ID = 2, IDroom = 2, ReceiveDate = DateTime.Now, CheckOutDate = DateTime.Now, RoomMoney = 100, ElectricMoney = 100, WaterMoney = 100, ServiceMoney = 100, Active = Active.Yes });
            modelbuilder.Entity<Room>().HasData(
                new Room { ID = 1, Name = "A201", Quantity = 4, Staked = 0 },
                new Room { ID = 2, Name = "A202", Quantity = 5, Staked = 0 },
                new Room { ID = 3, Name = "A203", Quantity = 3, Staked = 0 },
                new Room { ID = 4, Name = "A240", Quantity = 3, Staked = 0 });
            modelbuilder.Entity<RoomImage>().HasData(
                new RoomImage { ID = 1, IDroom = 1, Name = "A1 Image", Url = "D:jett.png" },
                new RoomImage { ID = 2, IDroom = 1, Name = "A2 Image", Url = "D:home.png" });
            modelbuilder.Entity<PeopleRental>().HasData(
                new PeopleRental { ID = 1, IDPeople = 2, IDRental = 1, Membership = Membership.Leader },
                new PeopleRental { ID = 2, IDPeople = 3, IDRental = 1, Membership = Membership.Member },
                new PeopleRental { ID = 3, IDPeople = 4, IDRental = 1, Membership = Membership.Member },
                new PeopleRental { ID = 4, IDPeople = 5, IDRental = 2, Membership = Membership.Leader }
                );
            modelbuilder.Entity<Statistics>().HasData(
                new Statistics { ID = 1, ElectricMoneyOfGovernment = 90, WaterMoneyOfGovernment = 90, ServiceOfGovernment = 90, Month = 10, Year = 2023 });
        }
    }
}