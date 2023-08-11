
using Imo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ImoService.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ImoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImovelController : ControllerBase
    {
        //private Guid idImovelSelectionado;
        //private string pathImage;
        private readonly ImoContext mvcImoContext;
        //private readonly IWebHostEnvironment mvcWebHostEnvironment;
        public ImovelController(ImoContext mvcImoContext, IWebHostEnvironment mvcWebHostEnvironment)
        {
            this.mvcImoContext = mvcImoContext;
            //this.mvcWebHostEnvironment = mvcWebHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imovel>>> GetImovel()
        {
            if(mvcImoContext.Imovels == null)
            {
                return NotFound();  
            }

            //var imoveis = await mvcImoContext.Imovels.ToListAsync();
            return await mvcImoContext.Imovels.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Imovel>> GetImovel(Guid id)
        {
            if (mvcImoContext.Imovels == null)
            {
                return NotFound();
            }

            var imoveis = await mvcImoContext.Imovels.FindAsync(id);
            if (imoveis == null)
            {
                return NotFound();
            }
            return imoveis;
        }

        [HttpPost]
        public async Task<ActionResult<Imovel>> PostImovel(Imovel imovel)
        {
            mvcImoContext.Imovels.Add(imovel);
            await mvcImoContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImovel), new { id = imovel.Id}, imovel);
        }

        [HttpPut]
        public async Task<ActionResult> PutImovel(Guid id, Imovel imovel)
        {
            if (id != imovel.Id)
            {
                return BadRequest();
            }
            mvcImoContext.Entry(imovel).State = EntityState.Modified;

            try
            {
                await mvcImoContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!ImovelAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();

        }

        private bool ImovelAvailable(Guid id)
        {
            return (mvcImoContext.Imovels?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteImovel(Guid id)
        {
            if (mvcImoContext.Imovels == null)
            {
                return NotFound();
            }

            var imovel = await mvcImoContext.Imovels.FindAsync(id);
            if (imovel == null)
            {
                return NotFound();
            }

            mvcImoContext.Imovels.Remove(imovel);
            await mvcImoContext.SaveChangesAsync();
            
            return Ok();

        }

        //[HttpPost]
        ////public IActionResult UploadImagem(IEnumerable<IFormFile> files)
        //public async Task<IActionResult> UploadImagem(IEnumerable<IFormFile> files, Int64 idImovel)
        //{
        //    IFormFile imagemCarregada = files.FirstOrDefault();
        //    if (imagemCarregada != null)
        //    {
        //        pathImage = mvcWebHostEnvironment.WebRootPath;


        //        string pathSaveImage = pathImage + "\\image\\";
        //        string newNameImage = imagemCarregada.FileName;

        //        if (!Directory.Exists(pathSaveImage))
        //        {
        //            Directory.CreateDirectory(pathSaveImage);
        //        }

        //        using (var stream = System.IO.File.Create(pathSaveImage + newNameImage))
        //        {
        //            imagemCarregada.CopyToAsync(stream);
        //        }


        //        MemoryStream ms = new MemoryStream();
        //        imagemCarregada.OpenReadStream().CopyTo(ms);

        //        Imovel_Foto file = new Imovel_Foto()
        //        {
        //            Id = Guid.NewGuid(),
        //            IdImovel = idImovel,
        //            DescricaoFoto = imagemCarregada.FileName,
        //            PathFoto = pathSaveImage + "\\" + imagemCarregada.FileName,
        //            ContentType = imagemCarregada.ContentType,
        //            Date = DateTime.Now,
        //            IdUser = 1
        //        };

        //        mvcImoContext.Imovel_Fotos.Add(file);
        //        mvcImoContext.SaveChanges();
        //    }




        //    return RedirectToAction("Image");
        //}
        //public IActionResult Visualizar(Guid id)
        //{
            
        //    var arquivosBanco = mvcImoContext.Imovel_Fotos.FirstOrDefault(a => a.Id == id);

        //    return File(arquivosBanco.PathFoto, arquivosBanco.DescricaoFoto);
        //}

        

    }
}
