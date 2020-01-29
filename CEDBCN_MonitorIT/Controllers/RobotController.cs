﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CEDBCN_MonitorIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEDBCN_MonitorIT.Controllers
{
    [Route("api/v1/[controller]")]
    public class RobotController : Controller
    {
        private readonly MonitorRobotsDbContext _context;

        public IActionResult Index()
        {
            return View();
        }

        

        public RobotController(MonitorRobotsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Robot> GetAllRobots()
        {

            return _context.Robot.Include(c => c.IDRobot).ToList();
        }
        [HttpGet("{id}")]
        public IActionResult GetByIdBook(int id)
        {

            var result = this._context.CasoRobot.Include(c => c.ID_CasoRobot).SingleOrDefault(t => t.ID_CasoRobot == id);
            if (id == 0)
            {
                return BadRequest();
            }
            else if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return new NoContentResult();
            }
        }
        [HttpPost]
        public IActionResult AddCaso([FromBody] Caso_Robot casos)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this._context.CasoRobot.Add(casos);
            this._context.SaveChanges();
            return Created($"books/{casos.ID_CasoRobot}", casos);
        }
        //[HttpPut("{id}")]
        //public IActionResult updateBooks(int id, [FromBody] Caso_Robot casos)
        //{
        //    var up = this._context.CasoRobot.FirstOrDefault(ct => ct.ID_CasoRobot == id);
        //    if (up == null)
        //    {
        //        return new NoContentResult();
        //    }
        //    else
        //    {
        //        up.Description = books.Description;
        //        up.Genre = books.Genre;
        //        up.Title = books.Title;
        //        up.Section = books.Section;
        //        up.Publisher = books.Publisher;
        //        up.Year = books.Year;

        //        _context.SaveChanges();
        //        return Ok(up);
        //    }
        //}
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var target = _context.CasoRobot.FirstOrDefault(ct => ct.ID_CasoRobot == id);
            if (target == null)
            {
                return NotFound();
            }
            else
            {
                _context.CasoRobot.Remove(target);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}