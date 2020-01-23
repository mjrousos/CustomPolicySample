using Microsoft.AspNetCore.Authorization;

namespace CustomPolicyProvider
{
    internal class ProjectRequirement : IAuthorizationRequirement
    {
        public string ProjectName{ get; private set; }

        public ProjectRequirement(string projectName) { ProjectName = projectName; }
    }
}