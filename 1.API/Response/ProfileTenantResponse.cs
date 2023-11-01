﻿using _3.Data.Model;

namespace _1.API.Response;
 
 public class ProfileResponseOwner
 {
     public int Id { get; set; }
     public string Name { get; set; }
     public string Lastname { get; set; }
     public string Email { get; set; }
     public UserType UserType{ get; set; }
     public string Phone { get; set; }
     public int QuantityVehiclesRented { get; set; }
     public List<Automobile> Automobiles { get; set; }
     public byte[] Image { get; set; }
 }