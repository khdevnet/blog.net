﻿using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Web.Application.Service.Entity;
using Blog.Web.Application.Storage;

namespace Blog.Web.Application.Service.Internal
{
    public class EntryService : IEntryService
    {
        private readonly IUserService _userService;
        private readonly IRepository _repository;

        public EntryService(IUserService userService, IRepository repository)
        {
            _userService = userService;
            _repository = repository;
        }

        public void Save(Entry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Slug))
                throw new ArgumentNullException("entry", "Entry must have a Slug value to Save()");

            entry.Slug = entry.Slug.ToLowerInvariant();

            if (entry.DateCreated == default(DateTime))
            {
                var isUpdate = _repository.Exists<Entry>(entry.Slug);
                if (isUpdate)
                {
                    var oldEntry = _repository.Single<Entry>(entry.Slug);
                    entry.DateCreated = oldEntry.DateCreated;                    
                }
                else
                {
                    entry.DateCreated = DateTime.Now;
                }
            }

            entry.Author = _userService.Current.FriendlyName;

            _repository.Save(entry);
        }

        public Entry GetBySlug(string slug)
        {
            return _repository.Single<Entry>(slug);
        }

        public List<Entry> GetList()
        {
            return _repository.All<Entry>().ToList();
        }

        public void Delete(string slug)
        {
            _repository.Delete<Entry>(slug);
        }

        public bool Exists(string slug)
        {
            return _repository.Exists<Entry>(slug);
        }
    }
}