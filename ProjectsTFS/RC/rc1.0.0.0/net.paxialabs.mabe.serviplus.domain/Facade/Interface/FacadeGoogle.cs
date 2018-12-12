using net.paxialabs.mabe.serviplus.domain.Business.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Interface
{
    public static class FacadeGoogle
    {
        public static EntityGoogleMapsDistanceMatrixResponse GetDistanceMatrix(ModelViewGoogleMapsDistanceMatrixRequest request)
        {
            return new BusinessGoogle().GetDistanceMatrix(request);
        }

        public static DateTime GetLocalDateTime(double latitude, double longitude, DateTime utcDate)
        {
            return new BusinessGoogle().GetLocalDateTime(latitude, longitude, utcDate);
        }
    }
}
