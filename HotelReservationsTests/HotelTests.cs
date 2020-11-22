using HotelReservations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HotelReservationsTests
{
    [TestClass]
    public class HotelTests
    {
        [TestMethod]
        public void TestAvailability1()
        {
            Hotel hotel = new Hotel(1);

            Assert.IsFalse(hotel.CheckAvailability(new Reservation(-4, 2)));
        }

        [TestMethod]
        public void TestAvailability2()
        {
            Hotel hotel = new Hotel(1);

            Assert.IsFalse(hotel.CheckAvailability(new Reservation(200, 400)));
        }

        [TestMethod]
        public void TestAvailability3()
        {
            Hotel hotel = new Hotel(3);

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(0, 5)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(7, 13)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(3, 9)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(5, 7)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(6, 6)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(0, 4)));

        }

        [TestMethod]
        public void TestAvailability4()
        {
            Hotel hotel = new Hotel(3);

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(1, 3)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(2, 5)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(1, 9)));

            Assert.IsFalse(hotel.CheckAvailability(new Reservation(0, 15)));

        }

        [TestMethod]
        public void TestAvailability5()
        {
            Hotel hotel = new Hotel(3);

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(1, 3)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(0, 15)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(1, 9)));

            Assert.IsFalse(hotel.CheckAvailability(new Reservation(2, 5)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(4, 9)));

        }
        [TestMethod]
        public void TestAvailability6()
        {
            Hotel hotel = new Hotel(2);

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(1, 3)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(0, 4)));

            Assert.IsFalse(hotel.CheckAvailability(new Reservation(2, 3)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(5, 5)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(4, 10)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(10, 10)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(6, 7)));

            Assert.IsFalse(hotel.CheckAvailability(new Reservation(8, 10)));

            Assert.IsTrue(hotel.CheckAvailability(new Reservation(8, 9)));
        }
    }
}
