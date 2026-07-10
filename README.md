# Dashkevich Studio

Astro frontend and ASP.NET Core backend for `dashkevichstudio.by`.

## Architecture

- Astro static frontend.
- ASP.NET Core 10 Web API using Clean Architecture.
- PostgreSQL through EF Core and repository abstractions.
- Telegram delivery through transactional outbox and a background worker.
- Caddy reverse proxy with automatic HTTPS.
- Docker Compose deployment on one Yandex Cloud VM.

## Local frontend

```bash
pnpm install
pnpm dev
```

For a local API, create `.env.development.local`:

```dotenv
PUBLIC_API_BASE_URL=http://127.0.0.1:8080
```

## Full stack with Docker

```bash
cp .env.example .env
# Set strong PostgreSQL password, Telegram token and chat ID.
docker compose up --build -d
```

The website opens at `https://dashkevichstudio.by` after DNS points to the VM.

## Telegram bot

1. Open `@BotFather` in Telegram.
2. Run `/newbot`, set bot name and username.
3. Store the token in `.env` as `TELEGRAM_BOT_TOKEN`. Never commit it.
4. Open the new bot and press **Start** or send `/start`.
5. Open `https://api.telegram.org/bot<TOKEN>/getUpdates` in a browser.
6. Find `message.chat.id` and store it as `TELEGRAM_CHAT_ID`.
7. Restart API: `docker compose up -d --force-recreate api`.

For a group chat, add the bot to the group, send one message, then read the negative group `chat.id` from `getUpdates`.

## Yandex Cloud VM

Recommended minimum for MVP: Ubuntu 24.04, 2 vCPU, 2 GB RAM, 20 GB SSD.

1. Reserve a static public IP.
2. Allow inbound TCP ports `22`, `80`, and `443`. Do not expose `5432` or `8080`.
3. Install Git and Docker Engine with Docker Compose plugin.
4. Clone the repository to `/opt/dashkevich-studio`.
5. Create `.env` from `.env.example` and fill secrets.
6. Point Hoster.by `A` records for `@` and `www` to the VM IP.
7. Run `docker compose up --build -d`.
8. Caddy obtains and renews HTTPS certificates automatically.

## Backups

Run `deploy/backup-postgres.sh` daily from cron. It keeps 14 days of PostgreSQL dumps. Copy backups off the VM periodically; a backup on the same disk does not protect against disk loss.

Example cron entry:

```cron
15 3 * * * cd /opt/dashkevich-studio && set -a && . ./.env && set +a && PROJECT_DIR=/opt/dashkevich-studio ./deploy/backup-postgres.sh
```

## Checks

```bash
pnpm build
dotnet test backend/DashkevichStudio.slnx
```
