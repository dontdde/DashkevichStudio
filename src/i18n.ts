export const locales = ['en', 'ru', 'pl'] as const;
export type Locale = (typeof locales)[number];

export const languageNames: Record<Locale, string> = {
  en: 'English',
  ru: 'Русский',
  pl: 'Polski'
};

export function localePath(locale: Locale, path: string): string {
  const normalized = path.startsWith('/') ? path : `/${path}`;
  return locale === 'ru' ? normalized : `/${locale}${normalized}`;
}

export function localizedPathname(pathname: string, locale: Locale): string {
  const withoutLocale = pathname.replace(/^\/(en|ru|pl)(?=\/|$)/, '') || '/';
  return localePath(locale, withoutLocale);
}

export const commonCopy = {
  en: {
    skip: 'Skip to content',
    navLabel: 'Main navigation',
    nav: ['Home', 'Web Development', 'Business Automation', 'Cases'],
    contacts: 'Contacts',
    footer: 'Product design, development and business automation.',
    legalLabel: 'Legal documents',
    privacy: 'Privacy policy',
    offer: 'Public offer',
    requisites: 'Company details',
    language: 'Language',
    telegram: 'Contact us on Telegram',
    instagram: 'Open Instagram',
    themeDark: 'Dark',
    themeLight: 'Light',
    toDark: 'Switch to dark theme',
    toLight: 'Switch to light theme',
    requestLabel: 'Request',
    submit: 'Send request',
    submitting: 'Sending...',
    formTitle: 'Start a project',
    formIntro: 'Describe the task in your own words. We will clarify the details and suggest the next practical step.',
    name: 'Name',
    namePlaceholder: 'Your name',
    replyVia: 'How should we reply?',
    contactLabels: ['Your Telegram', 'Your Instagram', 'Your email', 'Your phone number', 'How can we contact you?'],
    contactPlaceholders: ['@username', '@username', 'name@example.com', '+375...', 'Preferred contact method'],
    contactOptions: ['Telegram', 'Instagram', 'Email', 'Phone', 'Other'],
    service: 'What do you need?',
    choose: 'Choose an option',
    serviceOptions: ['Website or landing page', 'Tilda / WordPress', 'Online store', 'Web service / customer portal', 'Telegram bot', 'CRM or automation', 'AI agent / AI assistant', 'Service integrations', 'Other'],
    customService: 'Your service option',
    customServicePlaceholder: 'For example: improve an existing product',
    description: 'Task description',
    descriptionPlaceholder: 'What needs to be done, who it is for, and what already exists',
    file: 'File or materials',
    consentBefore: 'I consent to personal data processing and accept the',
    consentLink: 'privacy policy',
    fileTooLarge: 'The file is too large. Maximum size is 10 MB.',
    sendError: 'We could not send the request. Please try again.',
    sendSuccess: 'Request sent. We will contact you shortly.',
    cookieBefore: 'We may use cookies and analytics to improve the website. By continuing, you agree to the',
    cookieAccept: 'Got it'
  },
  ru: {
    skip: 'Перейти к содержанию', navLabel: 'Основная навигация', nav: ['Главная', 'Веб-разработка', 'Автоматизация бизнеса', 'Кейсы'], contacts: 'Контакты',
    footer: 'Проектирование, разработка и автоматизация цифровых продуктов.', legalLabel: 'Юридические документы', privacy: 'Политика конфиденциальности', offer: 'Публичная оферта', requisites: 'Реквизиты', language: 'Язык',
    telegram: 'Связаться в Telegram', instagram: 'Перейти в Instagram',
    themeDark: 'Тёмная', themeLight: 'Светлая', toDark: 'Переключить тёмную тему', toLight: 'Переключить светлую тему', requestLabel: 'Заявка', submit: 'Отправить заявку', submitting: 'Отправляем...',
    formTitle: 'Оставить заявку', formIntro: 'Опишите задачу в свободной форме. Мы ответим, уточним детали и предложим следующий шаг.', name: 'Имя', namePlaceholder: 'Имя', replyVia: 'Куда ответить',
    contactLabels: ['Ваш Telegram', 'Ваш Instagram', 'Ваш email', 'Ваш номер телефона', 'Как с вами связаться'], contactPlaceholders: ['@username', '@username', 'name@example.com', '+375...', 'Удобный способ связи'], contactOptions: ['Telegram', 'Instagram', 'Email', 'Телефон', 'Свой вариант'],
    service: 'Что нужно сделать', choose: 'Выберите вариант', serviceOptions: ['Сайт или лендинг', 'Tilda / WordPress', 'Интернет-магазин', 'Веб-сервис / личный кабинет', 'Telegram-бот', 'CRM или автоматизация', 'AI-агент / AI-помощник', 'Интеграции с сервисами', 'Свой вариант'], customService: 'Свой вариант услуги', customServicePlaceholder: 'Например: доработать существующий проект',
    description: 'Описание задачи', descriptionPlaceholder: 'Что нужно сделать, для кого это, что уже есть сейчас', file: 'Файл или материалы', consentBefore: 'Я согласен(на) на обработку персональных данных и принимаю', consentLink: 'политику конфиденциальности', fileTooLarge: 'Файл слишком большой. Максимальный размер — 10 МБ.', sendError: 'Не удалось отправить заявку. Попробуйте ещё раз.', sendSuccess: 'Заявка отправлена. Мы скоро свяжемся с вами.', cookieBefore: 'Мы можем использовать cookie и аналитику, чтобы улучшать сайт. Продолжая пользоваться сайтом, вы соглашаетесь с', cookieAccept: 'Понятно'
  },
  pl: {
    skip: 'Przejdź do treści', navLabel: 'Główna nawigacja', nav: ['Strona główna', 'Tworzenie stron', 'Automatyzacja biznesu', 'Realizacje'], contacts: 'Kontakt',
    footer: 'Projektowanie, tworzenie i automatyzacja produktów cyfrowych.', legalLabel: 'Dokumenty prawne', privacy: 'Polityka prywatności', offer: 'Oferta publiczna', requisites: 'Dane firmy', language: 'Język',
    telegram: 'Napisz na Telegramie', instagram: 'Otwórz Instagram',
    themeDark: 'Ciemny', themeLight: 'Jasny', toDark: 'Włącz ciemny motyw', toLight: 'Włącz jasny motyw', requestLabel: 'Zapytanie', submit: 'Wyślij zapytanie', submitting: 'Wysyłanie...',
    formTitle: 'Rozpocznij projekt', formIntro: 'Opisz zadanie własnymi słowami. Doprecyzujemy szczegóły i zaproponujemy kolejny praktyczny krok.', name: 'Imię', namePlaceholder: 'Twoje imię', replyVia: 'Jak mamy odpowiedzieć?',
    contactLabels: ['Twój Telegram', 'Twój Instagram', 'Twój email', 'Twój numer telefonu', 'Jak możemy się skontaktować?'], contactPlaceholders: ['@username', '@username', 'name@example.com', '+48...', 'Preferowany sposób kontaktu'], contactOptions: ['Telegram', 'Instagram', 'Email', 'Telefon', 'Inny'],
    service: 'Czego potrzebujesz?', choose: 'Wybierz opcję', serviceOptions: ['Strona lub landing page', 'Tilda / WordPress', 'Sklep internetowy', 'Serwis webowy / panel klienta', 'Bot Telegram', 'CRM lub automatyzacja', 'Agent AI / asystent AI', 'Integracje z usługami', 'Inna opcja'], customService: 'Własny zakres usługi', customServicePlaceholder: 'Na przykład: rozbudowa istniejącego produktu',
    description: 'Opis zadania', descriptionPlaceholder: 'Co trzeba zrobić, dla kogo i co już istnieje', file: 'Plik lub materiały', consentBefore: 'Wyrażam zgodę na przetwarzanie danych osobowych i akceptuję', consentLink: 'politykę prywatności', fileTooLarge: 'Plik jest za duży. Maksymalny rozmiar to 10 MB.', sendError: 'Nie udało się wysłać zapytania. Spróbuj ponownie.', sendSuccess: 'Zapytanie zostało wysłane. Wkrótce się skontaktujemy.', cookieBefore: 'Możemy używać plików cookie i analityki, aby ulepszać stronę. Kontynuując, akceptujesz', cookieAccept: 'Rozumiem'
  }
} as const;
