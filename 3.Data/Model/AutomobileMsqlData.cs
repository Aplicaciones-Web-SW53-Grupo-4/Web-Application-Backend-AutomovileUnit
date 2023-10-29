﻿using System.Net.Http.Headers;
using _3.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace _3.Data.Model;

public class AutomobileMsqlData : IAutomobileData
{
    private AutomovileUnitBD _automobileUnitBd;
    
    public AutomobileMsqlData(AutomovileUnitBD atomobileUnitBd)
    {
        this._automobileUnitBd = atomobileUnitBd;
    }
    public Automobile GetById(long id)
    {
        return this._automobileUnitBd.TAutomobiles.Where(p=>p.Id == id).First();
    }
    
    public Automobile GetBySearch(long id,string brand,string model)
    {
        return this._automobileUnitBd.TAutomobiles.Where(p=>p.Id == id && p.Brand==brand&&p.Model==model).First();
    }

    public async Task<UserAutomovileResult> GetByUserAutomobile(int id, int automovileid)
    {
        var user = await this._automobileUnitBd.TUsers.Where(p => p.Id == id).FirstOrDefaultAsync();
        var automobile = await this._automobileUnitBd.TAutomobiles.Where(p => p.Id == automovileid).FirstOrDefaultAsync();

        if (user != null && automobile != null)
        {
            return new UserAutomovileResult
            {
                // User = user,
                Automobile = automobile
            };
        }
        return null; // Maneja el caso en el que no se encuentren datos
    }
    
    
    public Task<List<Automobile>> GetAllAsync()
    {
        return this._automobileUnitBd.TAutomobiles.ToListAsync();
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

    public bool Update(Automobile automobile, long id)
    {
        try
        {
            var automobilToUpdate = this._automobileUnitBd.TAutomobiles.Where(p=>p.Id == id).First();
            automobilToUpdate.Brand = automobile.Brand;
            automobilToUpdate.ClassType = automobile.ClassType;
            automobilToUpdate.Color = automobile.Color;
            automobilToUpdate.Description = automobile.Description;
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
    public bool Delete(long id)
    {
        try
        {
            var automobilToDelete = this._automobileUnitBd.TAutomobiles.Where(p=>p.Id == id).First();
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

    //Pendiente de implementar
    public Task<List<Automobile>> SearchCarByFilter(Automobile automobile)
    {
        return this._automobileUnitBd.TAutomobiles.Where((p) 
            => p.IsActive==true && p.Color==automobile.Color).ToListAsync();
    }
}