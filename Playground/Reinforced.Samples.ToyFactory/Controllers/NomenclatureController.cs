﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reinforced.Samples.ToyFactory.Dto.Nomenclature;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Channels.Queries;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Services;
using Reinforced.Samples.ToyFactory.Queries;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.Orm.Queries;

namespace Reinforced.Samples.ToyFactory.Controllers
{
    public class NomenclatureController : Controller
    {
        private readonly ILogger<NomenclatureController> _logger;
        private readonly ITecture _tecture;

        public NomenclatureController(ILogger<NomenclatureController> logger, ITecture tecture)
        {
            _logger = logger;
            _tecture = tecture;
        }

        [HttpPost]
        public ActionResult<int> CreateToyType([FromBody]CreateToyTypeDto req)
        {
            _tecture.BeginTrace();
            int result = 0;
            try
            {
                var a = _tecture.Do<Nomenclature>().CreateType(req.Name);
                _tecture.Save();
                var x = a;
                result = _tecture.From<Db>().Key(x);
            }
            finally
            {
                var t = _tecture.EndTrace();
                var text = t.Explain();
            }
            return result;
        }

        public class IdName
        {
            public IdName()
            {
                throw new Exception("Hha!");
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }
        [HttpGet]
        public async Task<IdName[]> All()
        {
            _tecture.BeginTrace(true);
            try
            {
                var result =
                    await _tecture.From<Db>().Get<ToyType>()
                        .All
                        .Select(x => new IdName()
                        {
                            Id = (int)(object)x.Name,
                            Name = x.Name,

                        }).ToArrayAsync();



                return result;
            }
            finally
            {
                var trace = _tecture.EndTrace();
                var s = trace.Explain();
            }
        }

        [HttpGet]
        public async Task<ToyType> GetToyType(int id)
        {
            _tecture.BeginTrace();
            var r = await _tecture.From<Db>().Get<ToyType>().ByIdAsync(id);
            var trc = _tecture.EndTrace();
            var text = trc.Explain();
            return r;
        }
    }
}
