﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace net.paxialabs.mabe.serviplus.resource.Common {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceMessage() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("net.paxialabs.mabe.serviplus.resource.Common.ResourceMessage", typeof(ResourceMessage).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MABE Serviplus.
        /// </summary>
        public static string AppTitle {
            get {
                return ResourceManager.GetString("AppTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Correo electrónico incorrecto..
        /// </summary>
        public static string EmailInvalid {
            get {
                return ResourceManager.GetString("EmailInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Saludos #%Nombre%#,
        ///
        ///En respuesta a su solicitud de recuperación de clave de acceso.
        ///
        ///Le hacemos llegar su clave temporal 
        ///
        ///#%ClaveTemporal%#
        ///
        ///Le sugerimos cambiar la clave temporal
        ///
        ///Mabe ServiPlus.
        /// </summary>
        public static string NotificationRecoveryBodyContent {
            get {
                return ResourceManager.GetString("NotificationRecoveryBodyContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MABE Serviplus Recuperación de contraseña.
        /// </summary>
        public static string NotificationRecoveryPassword {
            get {
                return ResourceManager.GetString("NotificationRecoveryPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Clave actual incorrecta..
        /// </summary>
        public static string PasswordOldInvalid {
            get {
                return ResourceManager.GetString("PasswordOldInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usuario incorrecto..
        /// </summary>
        public static string UserInvalid {
            get {
                return ResourceManager.GetString("UserInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usuario y/o Clave incorrecta..
        /// </summary>
        public static string UserPasswordInvalid {
            get {
                return ResourceManager.GetString("UserPasswordInvalid", resourceCulture);
            }
        }
    }
}
