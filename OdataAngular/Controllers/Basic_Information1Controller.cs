using System;
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
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using System.Web.OData.Routing;
using OdataAngular;

namespace OdataAngular.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using OdataAngular;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Basic_Information>("Basic_Information1");
    builder.EntitySet<Class>("Classes"); 
    builder.EntitySet<Department>("Departments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class Basic_Information1Controller : ODataController
    {
        private DomainModel db = new DomainModel();

        // GET: odata/Basic_Information1
        [EnableQuery]
        public IQueryable<Basic_Information> GetBasic_Information1()
        {
            return db.Basic_Information;
        }

        // GET: odata/Basic_Information1(5)
        [EnableQuery]
        public SingleResult<Basic_Information> GetBasic_Information([FromODataUri] int key)
        {
            return SingleResult.Create(db.Basic_Information.Where(basic_Information => basic_Information.Id == key));
        }

        // PUT: odata/Basic_Information1(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Basic_Information> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Basic_Information basic_Information = await db.Basic_Information.FindAsync(key);
            if (basic_Information == null)
            {
                return NotFound();
            }

            patch.Put(basic_Information);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Basic_InformationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(basic_Information);
        }

        // POST: odata/Basic_Information1
        public async Task<IHttpActionResult> Post(Basic_Information basic_Information)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Basic_Information.Add(basic_Information);
            await db.SaveChangesAsync();

            return Created(basic_Information);
        }

        // PATCH: odata/Basic_Information1(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Basic_Information> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Basic_Information basic_Information = await db.Basic_Information.FindAsync(key);
            if (basic_Information == null)
            {
                return NotFound();
            }

            patch.Patch(basic_Information);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Basic_InformationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(basic_Information);
        }

        // DELETE: odata/Basic_Information1(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Basic_Information basic_Information = await db.Basic_Information.FindAsync(key);
            if (basic_Information == null)
            {
                return NotFound();
            }

            db.Basic_Information.Remove(basic_Information);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Basic_Information1(5)/Class
        [EnableQuery]
        public SingleResult<Class> GetClass([FromODataUri] int key)
        {
            return SingleResult.Create(db.Basic_Information.Where(m => m.Id == key).Select(m => m.Class));
        }

        // GET: odata/Basic_Information1(5)/Department
        [EnableQuery]
        public SingleResult<Department> GetDepartment([FromODataUri] int key)
        {
            return SingleResult.Create(db.Basic_Information.Where(m => m.Id == key).Select(m => m.Department));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Basic_InformationExists(int key)
        {
            return db.Basic_Information.Count(e => e.Id == key) > 0;
        }


    }
}
