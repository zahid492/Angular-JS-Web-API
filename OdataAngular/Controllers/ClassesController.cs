﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using OdataAngular;

namespace OdataAngular.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using OdataAngular;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Class>("Classes");
    builder.EntitySet<Basic_Information>("Basic_Information"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ClassesController : ODataController
    {
        private DomainModel db = new DomainModel();

        // GET: odata/Classes
        [EnableQuery]
        public IQueryable<Class> GetClasses()
        {
            return db.Classes;
        }

        // GET: odata/Classes(5)
        [EnableQuery]
        public SingleResult<Class> GetClass([FromODataUri] int key)
        {
            return SingleResult.Create(db.Classes.Where(@class => @class.Id == key));
        }

        // PUT: odata/Classes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Class> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Class @class = await db.Classes.FindAsync(key);
            if (@class == null)
            {
                return NotFound();
            }

            patch.Put(@class);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(@class);
        }

        // POST: odata/Classes
        public async Task<IHttpActionResult> Post(Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Classes.Add(@class);
            await db.SaveChangesAsync();

            return Created(@class);
        }

        // PATCH: odata/Classes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Class> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Class @class = await db.Classes.FindAsync(key);
            if (@class == null)
            {
                return NotFound();
            }

            patch.Patch(@class);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(@class);
        }

        // DELETE: odata/Classes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Class @class = await db.Classes.FindAsync(key);
            if (@class == null)
            {
                return NotFound();
            }

            db.Classes.Remove(@class);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Classes(5)/Basic_Information
        [EnableQuery]
        public IQueryable<Basic_Information> GetBasic_Information([FromODataUri] int key)
        {
            return db.Classes.Where(m => m.Id == key).SelectMany(m => m.Basic_Information);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassExists(int key)
        {
            return db.Classes.Count(e => e.Id == key) > 0;
        }
    }
}
