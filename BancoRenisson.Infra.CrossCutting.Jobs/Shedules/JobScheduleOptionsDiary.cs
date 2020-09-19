namespace Envolva.Infra.CrossCutting.Jobs.Schedules.Contracts
{
    public class JobScheduleOptionsDiary
    {
        public int TimeToUpdate { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
    }
}