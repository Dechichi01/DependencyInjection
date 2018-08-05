using System;

namespace Framework.DI
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InstallAttribute : Attribute
    {
        private readonly Type type;

        public Type InstallType { get { return type; } }

        public InstallAttribute(Type type)
        {
            this.type = type;
        }
    }
}
