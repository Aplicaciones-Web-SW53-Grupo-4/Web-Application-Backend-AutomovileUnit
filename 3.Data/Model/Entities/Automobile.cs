﻿namespace _3.Data.Model;

public class Automobile:ModelBase
{
    public string Brand { get; set; }
    public double Price { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public AutomovilTransmissionType TransmissionType { get; set; }
    public AutomovilClassType ClassType { get; set; }
    public bool IsAvailable { get; set; }
}