﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.Interfaces;
using TimetableBusinessLogic.ViewModels;
using TimetableDatabaseImplement.Models;

namespace TimetableDatabaseImplement.Implements
{
    public class DenearyStorage : IDenearyStorage
    {
        public List<DenearyViewModel> GetFullList()
        {
            using (var context = new TimetableDatabase())
            {
                return context.Denearies
                .Select(rec => new DenearyViewModel
                {
                    Id = rec.Id,
                    Login = rec.Login,
                    Name = rec.Name,
                    Email = rec.Email,
                    Password = rec.Password
                }).ToList();
            }
        }
        public List<DenearyViewModel> GetFilteredList(DenearyBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                return context.Denearies
                .Where(rec => rec.Login == model.Login && rec.Password == model.Password)
                .Select(rec => new DenearyViewModel
                {
                    Id = rec.Id,
                    Login = rec.Login,
                    Name = rec.Name,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }
        public DenearyViewModel GetElement(DenearyBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                var Deneary = context.Denearies
                .FirstOrDefault(rec => rec.Login == model.Login || rec.Id == model.Id);
                return Deneary != null ?
                new DenearyViewModel
                {
                    Id = Deneary.Id,
                    Login = Deneary.Login,
                    Name = Deneary.Name,
                    Email = Deneary.Email,
                    Password = Deneary.Password,
                } :
                null;
            }
        }
        public void Insert(DenearyBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                context.Denearies.Add(CreateModel(model, new Deneary()));
                context.SaveChanges();
            }
        }
        public void Update(DenearyBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                var element = context.Denearies.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(DenearyBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                Deneary element = context.Denearies.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Denearies.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Deneary CreateModel(DenearyBindingModel model, Deneary Deneary)
        {
            Deneary.Name = model.Name;
            Deneary.Password = model.Password;
            Deneary.Login = model.Login;
            Deneary.Email = model.Email;
            return Deneary;
        }
    }
}
