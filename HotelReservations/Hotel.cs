using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservations
{
    public class Program
    {
        public static void Main(string[] args)
        {

        }
    }
    /// <summary>
    /// function @Hotel/CheckAvailability only checks if booking request is possible
    /// system does not saving reservations and really does not know which room is assigned to every reservation 
    /// but it's written in that way just for sake of showing the design, however it would be easy to change this system to work like that
    /// </summary>
    public class Hotel
    {
        private readonly IReservationService reservationSystem;
        private readonly ICollection<Room> rooms;
        private readonly RoomService roomService;
        /// <summary>
        /// creates an hotel reservation system 
        /// </summary>
        /// <param name="roomCount">hotel reservation system will be in charge of 'roomCount' rooms</param>
        public Hotel(int roomCount)
        {
            this.rooms = new List<Room>();
            this.roomService = new RoomService();

            //get rooms of particular type
            rooms = roomService.PrepareRooms(RoomType.SingleRoom, roomCount);

            //init reservation service
            reservationSystem = new HotelReservationService(roomCount);
        }

        /// <summary>
        /// following method adds room to hotel
        /// </summary>
        /// <param name="roomType">specific room type</param>
        public void AddRoom(RoomType roomType) {
            rooms.Add(roomService.PrepareRoom(roomType));
        }

        /// <summary>
        /// following method checks if booking request is possible and if so will create add a reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public bool CheckAvailability(Reservation reservation)
        {
            return reservationSystem.IsAvailable(reservation);
        }
    }
    public class RoomService
    {
        private static int roomNumber = 0;

        /// <summary>
        /// returns multiple rooms of same type
        /// </summary>
        /// <param name="roomType"></param>
        /// <param name="roomCount"></param>
        /// <returns></returns>
        public ICollection<Room> PrepareRooms(RoomType roomType, int roomCount)
        {
            ICollection<Room> rooms = new List<Room>();

            for (int i = 0; i < roomCount; i++)
                rooms.Add(PrepareRoom(roomType));

            return rooms;
        }

        /// <summary>
        /// returns single room of certain type
        /// </summary>
        /// <param name="roomType"></param>
        /// <returns></returns>
        public Room PrepareRoom(RoomType roomType)
        {
            switch (roomType){
                case RoomType.SingleRoom: return new SingleRoom(++roomNumber);
                default: return new SingleRoom(++roomNumber);
            }
        }
    }

    public interface IReservationService
    {
        bool IsAvailable(Reservation reservation);
        void ConfirmReservation(Reservation reservation);
    }

    public class HotelReservationService : IReservationService
    {
        //if we need to assing room to particular reservation and not just to check if we can perform that reservation
        //we need to keep track of created reservations and all rooms in hotel for that purpuses
        private ICollection<Reservation> reservations;
        private ICollection<Room> rooms;

        /// <summary>
        /// key represents day, value represents number of rooms booked for that particular date
        /// </summary>
        private int[] calendar;

        private readonly int roomCount;

        public HotelReservationService(int roomCount)
        {
            InitCalendar();
            this.roomCount = roomCount;
        }

        /// <summary>
        /// initialization of calendar structure
        /// </summary>
        /// <param name="days"></param>
        private void InitCalendar(int days = 366)
        {
            calendar = new int [days];
        }

        /// <summary>
        /// following method checks if booking request can be accepted
        /// </summary>
        /// <param name="reservation">reservation is the reservation object to be checked</param>
        /// <returns>true/false</returns>
        public bool IsAvailable(Reservation reservation)
        {
            //check if parameters of the request are valid
            if (!ValidateBookingRequest(reservation))
                return false;

            return TrySchedule(reservation);
        }

        private bool ValidateBookingRequest(Reservation reservation)
        {
            return reservation.StartDate <= reservation.EndDate 
                && reservation.StartDate >= 0 
                && reservation.EndDate <= 365 
                && reservation.StartDate <= 365
                && reservation.EndDate >= 0;
        }

        /// <summary>
        /// following is checking if booking request is possible based on the fact 
        /// that booking is always possible if there is at least one free room for every day requested in the reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        private bool TrySchedule(Reservation reservation)
        {
            for (int i = reservation.StartDate; i <= reservation.EndDate; i++)
                if (calendar[i] == roomCount)
                    return false;
            ConfirmReservation(reservation);
            return true;

        }

        /// <summary>
        /// following method updates calendar for confirmed reservation
        /// </summary>
        /// <param name="reservation">reservation is the confirmed reservation object</param>
        /// <returns></returns>
        public void ConfirmReservation(Reservation reservation)
        {
            for (int i = reservation.StartDate; i <= reservation.EndDate; i++)
                calendar[i]++;
        }
    }

    public class Reservation
    {
        public Reservation(int startDate, int endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public int StartDate { get; }
        public int EndDate { get; }
        public Room Room { get; set; }
    }
    
    public class SingleRoom : Room
    {
        //added number of beds just for visualization
        public SingleRoom(int roomNumber)
        {
            this.roomNumber = roomNumber;
            this.numberOfBeds = (int)RoomType.SingleRoom;
        }
        protected override string Info()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("This is the room with number ").Append(roomNumber).Append(" and has ").Append(NumberOfBeds).Append(" bed");
            return sb.ToString();
        }
    }

    public abstract class Room
    {
        protected int roomNumber;
        protected int numberOfBeds;
        //here you can have more common fields

        public int NumberOfBeds => numberOfBeds;
        public int RoomNumber => roomNumber;
        protected abstract string Info();
    }

    public enum RoomType
    {
        SingleRoom = 1
    }

}
