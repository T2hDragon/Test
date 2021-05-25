using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.App.EF.AppDataInit
{
    class EntityManager
    {
        private static AppDbContext _ctx = default!;

        public EntityManager(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        private async Task<LangString> CreateLangString(string valueEn, string valueEe)
        {
            var langString = new LangString
            {
                Translations = new List<Translation>()
            };

            var englishTranslation = new Translation
            {
                Culture = "en",
                Value = valueEn,
                LangString = langString
            };
            await _ctx.Translations.AddAsync(englishTranslation);
            
            var estoniaTranslation = new Translation
            {
                Culture = "et",
                Value = valueEe,
                LangString = langString
            };
            await _ctx.Translations.AddAsync(estoniaTranslation);
            await _ctx.LangStrings.AddAsync(langString);
            await _ctx.SaveChangesAsync();
            return langString;
        }

        

    }
}