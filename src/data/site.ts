export const site = {
  name: 'Dashkevich Studio',
  url: 'https://dashkevichstudio.by',
  email: 'hello@dashkevichstudio.by',
  phoneLabel: '+375 25 683 25 42',
  phoneHref: 'tel:+375256832542',
  telegramLabel: '@dashkevich_studio',
  telegramUrl: 'https://t.me/dashkevich_studio',
  instagramLabel: '@dashkevich_studio',
  instagramUrl: 'https://www.instagram.com/dashkevich_studio/',
  description:
    'Dashkevich Studio проектирует и запускает сайты, лендинги, интернет-магазины, WordPress/Tilda-доработки, автоматизацию и AI-инструменты для бизнеса.',
  nav: [
    { href: '/', label: 'Главная' },
    { href: '/development/', label: 'Разработка' },
    { href: '/ai-agents/', label: 'AI-агенты' },
    { href: '/cases/', label: 'Кейсы' }
  ]
};

export const services = [
  'Лендинг или сайт для заявок',
  'Tilda и WordPress: доработка, перенос, запуск',
  'Интернет-магазин, каталог и онлайн-оплата',
  'Личный кабинет или веб-платформа',
  'Backend и база данных',
  'Интеграции с CRM и сервисами',
  'Telegram-боты для клиентов и команды',
  'Формы, заявки, уведомления и лиды',
  'Автоматизация, API, парсеры и расчёты',
  'Поддержка после запуска'
];

export const upcomingCases = [
  'Новый проект в работе',
  'Следующий кейс готовится'
];

export const contactMethods = [
  { value: 'telegram', label: 'Telegram', fieldLabel: 'Ваш Telegram', placeholder: '@username' },
  { value: 'instagram', label: 'Instagram', fieldLabel: 'Ваш Instagram', placeholder: '@username' },
  { value: 'email', label: 'Email', fieldLabel: 'Ваш email', placeholder: 'name@example.com' },
  { value: 'phone', label: 'Телефон', fieldLabel: 'Ваш номер телефона', placeholder: '+375...' },
  { value: 'custom', label: 'Свой вариант', fieldLabel: 'Как с вами связаться', placeholder: 'Удобный способ связи' }
];

export const requestServices = [
  'Сайт или лендинг',
  'Tilda / WordPress',
  'Интернет-магазин',
  'Веб-сервис / личный кабинет',
  'Telegram-бот',
  'CRM или автоматизация',
  'AI-агент / AI-помощник',
  'Интеграции с сервисами',
  'Свой вариант'
];

export const demandInsights = [
  {
    title: 'Лендинги и сайты для заявок',
    text: 'Продающая страница, форма заявки, понятная структура, адаптив под телефон и быстрый запуск.'
  },
  {
    title: 'Tilda и WordPress',
    text: 'Доработка готового сайта, перенос, новые страницы, правки меню, форм, блоков и мобильной версии.'
  },
  {
    title: 'Каталог, магазин и оплата',
    text: 'Карточки товаров, корзина, онлайн-оплата, заявки из каталога и удобное управление контентом.'
  },
  {
    title: 'Автоматизация и боты',
    text: 'Telegram-боты, CRM-связки, уведомления, API, расчёты, таблицы и процессы без ручного переноса данных.'
  }
];

export const cases = [
  {
    title: 'Сайт KITTENNIS.BY для теннисного клуба',
    category: 'Спортивный клуб',
    direction: 'Сайт под ключ',
    summary:
      'Создали новую версию сайта клуба интересного тенниса в Минске: первый экран, КИТ Дивизион, результаты матчей, архив сезонов, турниры, партнёры и контакты.',
    result:
      'Клуб получил современный сайт с выразительной спортивной подачей, мобильной адаптацией, светлой и тёмной темой и более лёгкими изображениями.',
    highlights: [
      'Светлая и тёмная тема',
      'КИТ Дивизион и архив сезонов',
      'Последние результаты матчей',
      'Мобильное бургер-меню',
      'Оптимизация PNG-фонов в JPG'
    ],
    details: [
      {
        title: 'Задача',
        text:
          'Собрать новую визуальную и техническую версию сайта клуба, сохранив узнаваемость KIT Love to Win, теннисную эстетику и важные разделы для участников.'
      },
      {
        title: 'Что сделали',
        text:
          'Спроектировали главный экран, оформили последние результаты, таблицы Дивизиона, архив сезонов, турниры, устав, партнёров и контакты.'
      },
      {
        title: 'Дизайн',
        text:
          'Визуальная концепция построена вокруг теннисного корта, мяча, ракетки и клубной спортивной эстетики. Первый экран делает акцент на участии в Дивизионе.'
      },
      {
        title: 'Техника',
        text:
          'Проект собран как статический сайт на HTML, CSS и JavaScript. Тяжёлые PNG-фоны сжаты и заменены на более лёгкие JPG.'
      }
    ]
  }
];
