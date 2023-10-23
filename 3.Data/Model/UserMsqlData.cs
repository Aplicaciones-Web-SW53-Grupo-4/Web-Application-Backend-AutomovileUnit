﻿using _3.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace _3.Data.Model;

public class UserMsqlData :IUserData
{
    private AutomovileUnitBD _automovileUnitBd;
    
    public UserMsqlData( AutomovileUnitBD automovileUnitBd)
    {
        _automovileUnitBd = automovileUnitBd;
    }
    public User GetById(int id)
    {
            
        return _automovileUnitBd.TUsers.Where(t => t.Id == id && t.IsActive).First();

    }
    public User GetByName(string name)
    {
        return _automovileUnitBd.TUsers.Where(t => t.Name ==name && t.IsActive).FirstOrDefault();
    }
    public async Task<List<User>> GetAllAsync()
    {
        return await _automovileUnitBd.TUsers.Where(t => t.IsActive).ToListAsync();
    }
    public bool Create(User user)
    {
        try
        {
            _automovileUnitBd.TUsers.Add(user);
            _automovileUnitBd.SaveChanges();
            return true;
        }
        catch (Exception error)
        {
            //Logear
            return false;
        }
    }
    public bool Update(User tuser, int id)
    {    try
        {
            var userToBeUpdated = _automovileUnitBd.TUsers.Where(t => t.Id == id).First();

            userToBeUpdated.Name = tuser.Name;
            userToBeUpdated.Lastname = tuser.Lastname;
            userToBeUpdated.Country = tuser.Country;
            userToBeUpdated.phone = tuser.phone;
            
            userToBeUpdated.DateUpdate = DateTime.Now;

            _automovileUnitBd.TUsers.Update(userToBeUpdated);
            _automovileUnitBd.SaveChanges();
            
            return true;
        }
        catch (Exception error)
        {
            //Logear
            return false;
        }
    }
    
    
    public bool Delete(int id)
    {  try
        {
            var userToBeUpdated = _automovileUnitBd.TUsers.Where(t => t.Id == id).First();
            
            userToBeUpdated.DateUpdate = DateTime.Now;
            userToBeUpdated.IsActive = false;

            //_learningCenterBd.Tutorials.Remove(tutorialToBeUpdated);/// ELimina física
            
            _automovileUnitBd.TUsers.Update(userToBeUpdated);
            _automovileUnitBd.SaveChanges();
            
            return true;
        }
        catch (Exception error)
        {
            //Logear
            return false;
        }
    }
}