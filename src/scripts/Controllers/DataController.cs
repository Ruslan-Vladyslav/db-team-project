using Microsoft.AspNetCore.Mvc;
using DBLaba6.MainDB;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DBLaba6.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly OpendatamodelContext _context;

        public DataController(OpendatamodelContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var data = await _context.Data.ToListAsync();

            return Ok(data);
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetDataId(int id)
        {
            var data = await _context.Data.Where(x => x.DataId == id).FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound($"Data with current ID '{id}' wasn't found in the database");
            }

            return Ok(data);
        }


        [HttpDelete("id")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var deldata = await _context.Data.Where(x => x.DataId == id).FirstOrDefaultAsync();

            if (deldata == null)
            {
                return NotFound("Data with such ID doesn't exist");
            }

            _context.Data.Remove(deldata);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Data deleted successfully.", userId = id });
        }


        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] DataModel data)
        {
            if (!ModelState.IsValid)
            {
                var errorMesgs = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return BadRequest(string.Join(", ", errorMesgs));
            }

            var existingData = await _context.Data.FirstOrDefaultAsync(x => x.DataId == data.DataId);

            if (existingData != null)
            {
                return Conflict("A data with the same id already exists.");
            }

            var newData = new Datum
            {
                DataId = data.DataId,
                Name = data.Name,
                Description = data.Description,
                Format = data.Format,
                Content = data.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CategoryId = data.CategoryId
            };

            await _context.Data.AddAsync(newData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddData), new { id = newData.DataId }, newData);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateData([FromBody] DataModel data)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return BadRequest(new { errors });
            }

            var existData = await _context.Data.FirstOrDefaultAsync(x => x.DataId == data.DataId);

            if (existData == null)
            {
                return NotFound($"Data with ID '{data.DataId}' not found.");
            }

            existData.DataId = data.DataId;
            existData.Name = data.Name;
            existData.Description = data.Description;
            existData.Format = data.Format;
            existData.Content = data.Content;
            existData.CreatedAt = DateTime.UtcNow;
            existData.UpdatedAt = DateTime.UtcNow;
            existData.CategoryId = data.CategoryId;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Data updated successfully.", DataId = data.DataId });
        }
    }
}
