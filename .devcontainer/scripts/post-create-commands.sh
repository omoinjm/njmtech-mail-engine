# Check for required environment variables
: "${GIT_USER_EMAIL:?Need to set GIT_USER_EMAIL}"
: "${GIT_USER_NAME:?Need to set GIT_USER_NAME}"
: "${DOTNET_PROJECT_PATH:?Need to set DOTNET_PROJECT_PATH}"

# Git config
chmod 600 $HOME/.ssh/git/id_ed25519_github
eval "$(ssh-agent -s)"
ssh-add $HOME/.ssh/git/id_ed25519_github

# Add to .bashrc
echo "chmod 600 \$HOME/.ssh/git/id_ed25519_github" >> ~/.bashrc
echo "eval \$(ssh-agent -s)" >> ~/.bashrc
echo "ssh-add \$HOME/.ssh/git/id_ed25519_github" >> ~/.bashrc


git config --global --add safe.directory "$(pwd)"

git config --global user.email "$GIT_USER_EMAIL" && 
git config --global user.name "$GIT_USER_NAME"

# .NET config
dotnet restore "$DOTNET_PROJECT_PATH" || { echo "Dotnet restore failed"; exit 1; }
dotnet clean "$DOTNET_PROJECT_PATH" || { echo "Dotnet clean failed"; exit 1; }
dotnet build "$DOTNET_PROJECT_PATH" || { echo "Dotnet build failed"; exit 1; }
