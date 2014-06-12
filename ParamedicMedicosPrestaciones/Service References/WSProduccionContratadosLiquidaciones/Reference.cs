﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParamedicMedicosPrestaciones.WSProduccionContratadosLiquidaciones {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempuri.org", ConfigurationName="WSProduccionContratadosLiquidaciones.ContratadosLiquidacionesSoap")]
    public interface ContratadosLiquidacionesSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.GetCoordinaciones", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetCoordinaciones(long pUsr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.GetEstadoReclamo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetEstadoReclamo(string pItm, long pMode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.GetGuardiasDetalle", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetGuardiasDetalle(long pUsr, long pPer, long pCoo, long pVis);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.GetHorarios", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetHorarios(long pUsr, long pPer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.GetIncidentes", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetIncidentes(long pUsr, long pPer, long pCoo, long pVis);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.GetMedicos", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetMedicos(long pUsr, long pPer, long pVis);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.GetMotivosReclamo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetMotivosReclamo(long pClf);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.GetResumen", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetResumen(long pUsr, long pPer, long pCoo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.GetUsuarioValidacion", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetUsuarioValidacion(long pUsr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.SetReclamo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet SetReclamo(string pItm, long pMode, long pCnf, long pMot, string pHEnt, string pMEnt, string pHSal, string pMSal, string pObs, long pUsr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebServices.ContratadosLiquidaciones.SetRespuesta", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet SetRespuesta(string pItm, long pMode, long pSta, string pRta, long pUsr);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ContratadosLiquidacionesSoapChannel : ParamedicMedicosPrestaciones.WSProduccionContratadosLiquidaciones.ContratadosLiquidacionesSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ContratadosLiquidacionesSoapClient : System.ServiceModel.ClientBase<ParamedicMedicosPrestaciones.WSProduccionContratadosLiquidaciones.ContratadosLiquidacionesSoap>, ParamedicMedicosPrestaciones.WSProduccionContratadosLiquidaciones.ContratadosLiquidacionesSoap {
        
        public ContratadosLiquidacionesSoapClient() {
        }
        
        public ContratadosLiquidacionesSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ContratadosLiquidacionesSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContratadosLiquidacionesSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContratadosLiquidacionesSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet GetCoordinaciones(long pUsr) {
            return base.Channel.GetCoordinaciones(pUsr);
        }
        
        public System.Data.DataSet GetEstadoReclamo(string pItm, long pMode) {
            return base.Channel.GetEstadoReclamo(pItm, pMode);
        }
        
        public System.Data.DataSet GetGuardiasDetalle(long pUsr, long pPer, long pCoo, long pVis) {
            return base.Channel.GetGuardiasDetalle(pUsr, pPer, pCoo, pVis);
        }
        
        public System.Data.DataSet GetHorarios(long pUsr, long pPer) {
            return base.Channel.GetHorarios(pUsr, pPer);
        }
        
        public System.Data.DataSet GetIncidentes(long pUsr, long pPer, long pCoo, long pVis) {
            return base.Channel.GetIncidentes(pUsr, pPer, pCoo, pVis);
        }
        
        public System.Data.DataSet GetMedicos(long pUsr, long pPer, long pVis) {
            return base.Channel.GetMedicos(pUsr, pPer, pVis);
        }
        
        public System.Data.DataSet GetMotivosReclamo(long pClf) {
            return base.Channel.GetMotivosReclamo(pClf);
        }
        
        public System.Data.DataSet GetResumen(long pUsr, long pPer, long pCoo) {
            return base.Channel.GetResumen(pUsr, pPer, pCoo);
        }
        
        public System.Data.DataSet GetUsuarioValidacion(long pUsr) {
            return base.Channel.GetUsuarioValidacion(pUsr);
        }
        
        public System.Data.DataSet SetReclamo(string pItm, long pMode, long pCnf, long pMot, string pHEnt, string pMEnt, string pHSal, string pMSal, string pObs, long pUsr) {
            return base.Channel.SetReclamo(pItm, pMode, pCnf, pMot, pHEnt, pMEnt, pHSal, pMSal, pObs, pUsr);
        }
        
        public System.Data.DataSet SetRespuesta(string pItm, long pMode, long pSta, string pRta, long pUsr) {
            return base.Channel.SetRespuesta(pItm, pMode, pSta, pRta, pUsr);
        }
    }
}
