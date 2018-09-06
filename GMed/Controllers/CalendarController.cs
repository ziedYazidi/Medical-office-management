using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;

using GMed.Models;
namespace GMed.Controllers
{
    public class CalendarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            var scheduler = new DHXScheduler(this);

            /*
             * It's possible to use different actions of the current controller
             *      var scheduler = new DHXScheduler(this);     
             *      scheduler.DataAction = "ActionName1";
             *      scheduler.SaveAction = "ActionName2";
             * 
             * Or to specify full paths
             *      var scheduler = new DHXScheduler();
             *      scheduler.DataAction = Url.Action("Data", "Calendar");
             *      scheduler.SaveAction = Url.Action("Save", "Calendar");
             */

            /*
             * The default codebase folder is ~/Scripts/dhtmlxScheduler. It can be overriden:
             *      scheduler.Codebase = Url.Content("~/customCodebaseFolder");
             */



            scheduler.TimeSpans.Add(new DHXBlockTime()
            {
                StartDate = new DateTime(2011, 9, 14),
                EndDate = DateTime.Now,
            });
            scheduler.Highlighter.Enable("highlight_section", 60);
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;            
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ContentResult Data()
        {
            var data = new SchedulerAjaxData(new SampleDataContext().CalendarEvents);
            return (ContentResult)data;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            
            try
            {
                var changedEvent = (CalendarEvents)DHXEventsHelper.Bind(typeof(CalendarEvents), actionValues);
                var data = new SampleDataContext();
     

                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        //do insert
                        //action.TargetId = changedEvent.id;//assign postoperational id
                        data.CalendarEvents.InsertOnSubmit(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        //do delete
                        changedEvent = data.CalendarEvents.SingleOrDefault(ev => ev.id == action.SourceId);
                        data.CalendarEvents.DeleteOnSubmit(changedEvent);
                        break;
                    default:// "update"                          
                        //do update
                        var EventToUpdate = data.CalendarEvents.SingleOrDefault(ev => ev.id == action.SourceId);
                        DHXEventsHelper.Update(EventToUpdate, changedEvent, new List<string>() { "id" });
                        break;
                }
                data.SubmitChanges();
                action.TargetId = changedEvent.id;
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }
    }
}

