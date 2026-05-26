function truncate(text, maxLength, suffix = '...') {
  if (maxLength <= 0) throw new Error('maxLength debe ser mayor a 0.');
  if (text.length <= maxLength) return text;
  return text.slice(0, maxLength) + suffix;
}

function toSlug(text) {
  return text
    .toLowerCase()
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '') // eliminar tildes
    .replace(/[^a-z0-9\s-]/g, '')   // eliminar caracteres especiales
    .trim()
    .replace(/\s+/g, '-');           // espacios a guiones
}

function countWords(text) {
  if (!text || !text.trim()) return 0;
  return text.trim().split(/\s+/).length;
}

module.exports = { truncate, toSlug, countWords };
