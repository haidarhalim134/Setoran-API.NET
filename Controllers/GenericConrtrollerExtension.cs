using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Setoran_API.NET.Controllers;

public class GenericControllerEXtension<T> : ControllerBase where T : class
{
    [Authorize]
    [HttpGet("generic/{id}")]
    public ActionResult<T?> GetOne(Database db, [FromRoute] int id)
    {
        var entity = db.Set<T>().Find(id);
        if (entity is null)
            return NotFound(new {message="Item not found"});
        
        return entity;
    }

    [Authorize]
    [HttpDelete("generic/{id}")]
    public ActionResult Delete(Database db, [FromRoute] int id)
    {
        var entity = db.Set<T>().Find(id);
        if (entity is null)
            return NotFound(new {message="Item not found"});

        db.Remove(entity);
        db.SaveChanges();

        return Ok();
    }
}