﻿using System.Diagnostics;
using System.Net.Http.Headers;
using _3.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _3.Data.Model;

public class AutomobileMsqlData : IAutomobileData
{
    private AutomovileUnitBD _automobileUnitBd;

    public AutomobileMsqlData(AutomovileUnitBD atomobileUnitBd)
    {
        this._automobileUnitBd = atomobileUnitBd;
    }

    public Automobile GetById(string id)
    {
        return this._automobileUnitBd.TAutomobiles.Where(p => p.Id == id).First();
    }

    public Task<List<Automobile>> GetBySearch(string brand, string model)
    {
        return this._automobileUnitBd.TAutomobiles.Where(p => p.Brand == brand && p.Model == model).ToListAsync();
    }
    
    

    public async Task<UserAutomovileResult> GetByUserAutomobile(string id, string automovileid)
    {
        var user = await this._automobileUnitBd.TUsers.Where(p => p.Id == id).FirstOrDefaultAsync();
        var automobile = await this._automobileUnitBd.TAutomobiles.Where(p => p.Id == automovileid)
            .FirstOrDefaultAsync();

        if (user != null && automobile != null)
        {
            return new UserAutomovileResult
            {
                // User = user,
                Automobile = automobile
            };
        }

        return null; 
    }


    public Task<List<Automobile>> GetAllAsync()
    {
        return this._automobileUnitBd.TAutomobiles.Where(
            p => p.statusRequest == AutomobileRentStatus.Pending ||
                 p.statusRequest == AutomobileRentStatus.Rejected).ToListAsync();
    }

    public bool Create(Automobile automobile)
    {
        try
        {
            this._automobileUnitBd.TAutomobiles.Add(automobile);
            this._automobileUnitBd.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Update(Automobile automobile, string id)
    {
        try
        {
            var automobilToUpdate = this._automobileUnitBd.TAutomobiles.Where(p => p.Id == id).First();
            automobilToUpdate.Brand = automobile.Brand;
            automobilToUpdate.ClassType = automobile.ClassType;
            automobilToUpdate.Color = automobile.Color;
            automobilToUpdate.IsAvailable = automobile.IsAvailable;
            automobilToUpdate.Model = automobile.Model;
            this._automobileUnitBd.TAutomobiles.Update(automobilToUpdate);
            this._automobileUnitBd.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Delete(string id)
    {
        try
        {
            var automobilToDelete = this._automobileUnitBd.TAutomobiles.Where(p => p.Id == id).First();
            automobilToDelete.IsActive = false;
            this._automobileUnitBd.TAutomobiles.Update(automobilToDelete);
            this._automobileUnitBd.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public Task<List<Automobile>> SearchCarByFilter(Automobile automobile)
    {
        
        return _automobileUnitBd.TAutomobiles
            .Where(p =>
                (automobile.Brand == null || p.Brand == automobile.Brand || automobile.Brand=="") &&
                (automobile.Model==null || automobile.Model == "" || p.Model == automobile.Model)&&
                (automobile.Price == 0 || p.Price == automobile.Price)&&
                (automobile.ClassType == null || p.ClassType == automobile.ClassType)&&
                (automobile.QuantitySeat == 0 || p.QuantitySeat == automobile.QuantitySeat)&&
                (automobile.TransmissionType == null || p.TransmissionType == automobile.TransmissionType)&&
                (automobile.TimeRent == null || automobile.TimeRent=="" ||p.TimeRent == automobile.TimeRent)).ToListAsync();
    }

    public Task<List<Automobile>> GetCarsByOwnerID(string ownerId)
    {
        return this._automobileUnitBd.TAutomobiles.Where(p => p.UserId == ownerId).ToListAsync();
    }

}
