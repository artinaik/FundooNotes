using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        ILabelBL labelBL;
        public LabelsController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddLabel(string labelName, long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.CreateLabel(userID, noteID, labelName);
                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "Label added successfully" ,Response= result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Label already created" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpPut]
        public IActionResult RenameLabel(string lableName,string newLabelName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.RenameLabel(userID, lableName, newLabelName);
                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "Label renamed successfully",Response= result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpDelete]
        public IActionResult RemoveLabel(string lableName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (labelBL.RemoveLabel(userID, lableName))
                {
                    return this.Ok(new { success = true, message = "Label removed successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        public IActionResult RemoveLabelByNoteID(long noteID,string lableName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (labelBL.RemoveLabelByNoteID(userID, noteID,lableName))
                {
                    return this.Ok(new { success = true, message = "Label removed successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet]
        public IEnumerable GetLabelsByNoteID(long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                return labelBL.GetLabelsByNoteID(userID, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
