using System;

namespace Framework.DI
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InstallAttribute : Attribute
    {
        private readonly Type type;
        private readonly InstallMethod method;
        private readonly string id;

        public Type InstallType { get { return type; } }
        public InstallMethod InstallMethod { get { return method; } }
        public string Id { get { return id; } }

        public InstallAttribute(Type type, InstallMethod method = InstallMethod.Single, string id = "")
        {
            this.type = type;
            this.method = method;
            this.id = id;
        }
    }
}

public enum InstallMethod { Single, Transient, WithId }
