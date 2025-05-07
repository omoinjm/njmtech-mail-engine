# Check for required environment variables
: "${GIT_USER_EMAIL:?Need to set GIT_USER_EMAIL}"
: "${GIT_USER_NAME:?Need to set GIT_USER_NAME}"
: "${DOTNET_PROJECT_PATH:?Need to set DOTNET_PROJECT_PATH}"

# Git config
chmod 600 /root/.ssh/id_rsa
eval $(ssh-agent -s)
ssh-add /root/.ssh/id_rsa

# Add to .bashrc
echo "chmod 600 /root/.ssh/id_rsa" >> ~/.bashrc
echo "eval \$(ssh-agent -s)" >> ~/.bashrc
echo "ssh-add /root/.ssh/id_rsa" >> ~/.bashrc

git config --global --add safe.directory /workspaces

git config --global user.email "$GIT_USER_EMAIL" && 
git config --global user.name "$GIT_USER_NAME"

# .NET config
dotnet restore "$DOTNET_PROJECT_PATH" || { echo "Dotnet restore failed"; exit 1; }
dotnet clean "$DOTNET_PROJECT_PATH" || { echo "Dotnet clean failed"; exit 1; }
dotnet build "$DOTNET_PROJECT_PATH" || { echo "Dotnet build failed"; exit 1; }
