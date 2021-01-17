using System;
using AutomaticTelephoneStation.BillingSystem;

namespace AutomaticTelephoneStation.ATS.Interfaces
{
    public interface ITelephoneStation
    {
        event EventHandler<CallRecord> CallEndedEvent;
        void CallHandler(object sender, System.EventArgs e);
        void AnswerHandler(object sender, System.EventArgs e); 
        void DropHandler(object sender, System.EventArgs args);
    }
}