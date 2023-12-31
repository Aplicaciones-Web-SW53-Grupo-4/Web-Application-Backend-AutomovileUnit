﻿using System.ComponentModel.DataAnnotations;

namespace _3.Data.Model;

public class Automobile:ModelBase
{
    public string Brand { get; set; }
  
    public double Price { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    
    public int QuantitySeat{ get; set; }
    
    public AutomovilTransmissionType TransmissionType { get; set; }
    
    [Display(Name = "Class Type")] 
    public string ClassTypeString => ClassType.ToString();
    public AutomovilClassType ClassType { get; set; }
    public bool IsAvailable { get; set; }
    
    public string Place { get; set; }
    public string TimeRent { get; set; }

    [Display(Name = "status rent")] 
    public string statusRequestString => statusRequest.ToString();
    public AutomobileRentStatus statusRequest { get; set; }
    
    public string imageurl { get; set; }
    public string UserId { get; set; }
    private User User { get; set; }
}