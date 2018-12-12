using net.paxialabs.mabe.serviplus.entities.Entity.Security;


namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewGoogleMapsDistanceMatrixRequest : EntitySecurity
    {
        public string origins { get; set; }
        public string destinations { get; set; }
    }
}
