using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_visual_studio
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            string code;
            bool status = false;

            MYSQLRepository mySQLRepository = new();
            List<PersonModel> data = new();

            try
            {
                data = mySQLRepository.Read();
                code = ((int)ReturnCode.READ_SUCCESS).ToString();
                status = true;

            }
            catch (Exception ex)
            {
                code = ex.Message;
            }

            return Ok(new { status, code, data });
        }

        [HttpPost]
        public ActionResult Post()
        {
            bool status = false;
            string code;

            string name = Request.Form["name"];
            int age = Convert.ToInt32(Request.Form["age"]);

            MYSQLRepository mySQLRepository = new();

            try
            {
                mySQLRepository.Create(name, age);
                code = ((int)ReturnCode.CREATE_SUCCESS).ToString();
                status = true;
            }
            catch (Exception ex)
            {
                code = ex.Message;
            }

            return Ok(new { status, code });
        }

        [HttpPut]
        public ActionResult Put()
        {
            bool status = false;
            string code;

            int personId = Convert.ToInt32(Request.Form["personId"]);
            string name = Request.Form["name"];
            int age = Convert.ToInt32(Request.Form["age"]);

            MYSQLRepository mySQLRepository = new();

            try
            {
                mySQLRepository.Update(name, age, personId);
                code = ((int)ReturnCode.UPDATE_SUCCESS).ToString();
                status = true;
            }
            catch (Exception ex)
            {
                code = ex.Message;
            }

            return Ok(new { status, code });
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            bool status = false;
            string code;

            int personId = Convert.ToInt32(Request.Form["personId"]);

            MYSQLRepository mySQLRepository = new();

            try
            {
                mySQLRepository.Delete(personId);
                code = ((int)ReturnCode.DELETE_SUCCESS).ToString();
                status = true;
            }
            catch (Exception ex)
            {
                code = ex.Message;
            }

            return Ok(new { status, code });
        }
    }
}
