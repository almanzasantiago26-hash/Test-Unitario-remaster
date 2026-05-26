const { truncate, toSlug, countWords } = require('./stringHelper');

describe('StringHelper', () => {

  describe('truncate()', () => {
    it('devuelve el texto original si es menor o igual al límite', () => {
      expect(truncate('Hola', 10)).toBe('Hola');
    });

    it('devuelve el texto original si es exactamente igual al límite', () => {
      expect(truncate('Hola', 4)).toBe('Hola');
    });

    it('trunca el texto y agrega el sufijo por defecto', () => {
      expect(truncate('Hola Mundo', 4)).toBe('Hola...');
    });

    it('trunca el texto con sufijo personalizado', () => {
      expect(truncate('Hola Mundo', 4, '!')).toBe('Hola!');
    });

    it('lanza error si maxLength es 0', () => {
      expect(() => truncate('Hola', 0)).toThrow('maxLength debe ser mayor a 0.');
    });

    it('lanza error si maxLength es negativo', () => {
      expect(() => truncate('Hola', -5)).toThrow('maxLength debe ser mayor a 0.');
    });
  });

  describe('toSlug()', () => {
    it('convierte texto simple a slug', () => {
      expect(toSlug('Hola Mundo')).toBe('hola-mundo');
    });

    it('elimina caracteres especiales y tildes', () => {
      expect(toSlug('¡Hola Mundo! 2024')).toBe('hola-mundo-2024');
    });

    it('maneja múltiples espacios entre palabras', () => {
      expect(toSlug('hola   mundo')).toBe('hola-mundo');
    });

    it('devuelve string vacío si el texto está vacío', () => {
      expect(toSlug('')).toBe('');
    });

    it('elimina tildes correctamente', () => {
      expect(toSlug('Canción de Año Nuevo')).toBe('cancion-de-ano-nuevo');
    });
  });

  describe('countWords()', () => {
    it('cuenta palabras en un texto normal', () => {
      expect(countWords('Hola Mundo')).toBe(2);
    });

    it('maneja múltiples espacios entre palabras', () => {
      expect(countWords('Hola   Mundo   2024')).toBe(3);
    });

    it('devuelve 0 para texto vacío', () => {
      expect(countWords('')).toBe(0);
    });

    it('devuelve 0 para texto con solo espacios', () => {
      expect(countWords('     ')).toBe(0);
    });

    it('cuenta una sola palabra correctamente', () => {
      expect(countWords('Hola')).toBe(1);
    });
  });

});
