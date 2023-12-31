﻿using System.ComponentModel.DataAnnotations;
using _3.Data.Model;

namespace _1.API.Response;

public class AutomobileResponse

{
    public string Brand { get; set; }
  
    public double Price { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    
    public int QuantitySeat { get; set; }
    
    public AutomovilTransmissionType TransmissionType { get; set; }
    [Display(Name= "Class Type")] 
    public string ClassTypeString => ClassType.ToString();
    public AutomovilClassType ClassType { get; set; }
    public bool IsAvailable { get; set; }
    public string imageurl { get; set; }
    public string Place { get; set; }
    public string TimeRent { get; set; }
    public string statusRequestString { get; set; }
    public string Id { get; set; }

}