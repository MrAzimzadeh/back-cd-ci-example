## CI/CD Backend Deployment on AWS EC2 (Docker + GitHub Actions)

---

### ✨ Preparation (Local Machine)

```bash
# 1. Set PEM file permission
chmod 600 ~/Downloads/testv2.pem

# 2. Connect to EC2 instance
ssh -i ~/Downloads/testv2.pem ec2-user@****1.compute.amazonaws.com
```

---

### 🛠️ EC2 Server Configuration

```bash
# 3. Install Git
sudo yum install git -y

# 4. Update packages
sudo yum update -y

# 5. Install Docker
sudo yum install docker -y

# 6. Start Docker service
sudo service docker start

# 7. Add ec2-user to Docker group
sudo usermod -aG docker ec2-user

# (Important: Reconnect to apply group changes)
```

---

### 🔐 Generate SSH Key (for GitHub Deploy Key)

```bash
# 8. Generate a new SSH key
ssh-keygen -t rsa -b 4096 -C "deploy_key"

# 9. Show the public key to copy into GitHub Deploy Keys
cat ~/.ssh/id_rsa.pub
```

#### Add this to GitHub Repo > Settings > Deploy keys:
- Title: `EC2 Deploy Key`
- Paste the public key
- Check `Allow write access` if needed

---

### ⚖️ GitHub Secrets for GitHub Actions

| Secret Name    | Value                                             |
|----------------|---------------------------------------------------|
| `HOST`         | `****1.compute.amazonaws.com` |
| `USERNAME`     | `ec2-user`                                       |
| `PRIVATE_KEY`  | Contents of `~/.ssh/id_rsa` (Private key)         |

---

### 🛠️ Install Docker Compose on EC2

```bash
# 10. Download Docker Compose
sudo curl -L "https://github.com/docker/compose/releases/download/v2.33.1/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

# 11. Make it executable
sudo chmod +x /usr/local/bin/docker-compose

# 12. Check version
docker-compose version

sudo usermod -aG docker c2-user

newgrp docker

```

---

### 🧰 GitHub Actions Workflow (deploy.yml)

```yaml
name: Deploy to EC2

on:
  push:
    branches: [main]

jobs:
  deploy:
    runs-on: ubuntu-latest

    steps:
    - name: Checkout code
      uses: actions/checkout@v3

    - name: Deploy to EC2
      uses: appleboy/ssh-action@v1.0.3
      with:
        host: ${{ secrets.HOST }}
        username: ${{ secrets.USERNAME }}
        key: ${{ secrets.PRIVATE_KEY }}
        script: |
          cd ~/your-app-folder
          git pull origin main
          docker-compose down
          docker-compose up -d --build
```

---

**Optional:** Let me know if you want to add frontend setup, Nginx config, or SSL setup too.

