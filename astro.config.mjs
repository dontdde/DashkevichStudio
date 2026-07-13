import { defineConfig } from 'astro/config';
import sitemap from '@astrojs/sitemap';

export default defineConfig({
  site: 'https://dashkevichstudio.by',
  integrations: [sitemap({
    filter: (page) => !/\/(privacy|offer|requisites)\/$/.test(new URL(page).pathname),
    i18n: {
      defaultLocale: 'en',
      locales: { en: 'en', ru: 'ru', pl: 'pl' }
    }
  })],
  output: 'static'
});
