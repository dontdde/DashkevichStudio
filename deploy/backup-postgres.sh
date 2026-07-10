#!/usr/bin/env sh
set -eu

project_dir="${PROJECT_DIR:-/opt/dashkevich-studio}"
backup_dir="$project_dir/backups"
timestamp="$(date +%Y-%m-%d_%H-%M-%S)"

mkdir -p "$backup_dir"
cd "$project_dir"

docker compose exec -T postgres pg_dump \
  --username "$POSTGRES_USER" \
  --dbname "$POSTGRES_DB" \
  --format custom > "$backup_dir/postgres_$timestamp.dump"

find "$backup_dir" -type f -name 'postgres_*.dump' -mtime +14 -delete
