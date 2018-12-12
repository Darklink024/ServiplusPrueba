using System;
using System.Collections.Generic;


namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{
    [Serializable]
    public class EntityGoogleMapsDistanceMatrixResponse
    {
        public List<string> destination_addresses { get; set; }
        public List<string> origin_addresses { get; set; }
        public List<EntityGoogleMapsDistanceMatrixResponseRow> rows { get; set; }
        public string status { get; set; }
    }

    [Serializable]
    public class EntityGoogleMapsDistanceMatrixResponseRow
    {        
        public List<EntityGoogleMapsDistanceMatrixResponseElements> elements { get; set; }
    }

    [Serializable]
    public class EntityGoogleMapsDistanceMatrixResponseElements
    {
        public EntityGoogleMapsDistanceMatrixResponseDistance distance { get; set; }
        public EntityGoogleMapsDistanceMatrixResponseDuration duration { get; set; }
        public EntityGoogleMapsDistanceMatrixResponseDurationInTraffic duration_in_traffic { get; set; }
        public string status { get; set; }
    }

    [Serializable]
    public class EntityGoogleMapsDistanceMatrixResponseDistance
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    [Serializable]
    public class EntityGoogleMapsDistanceMatrixResponseDuration
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    [Serializable]
    public class EntityGoogleMapsDistanceMatrixResponseDurationInTraffic
    {
        public string text { get; set; }
        public string value { get; set; }
    }
}
