const { add, divide } = require('./mathHelper');

describe('add', () => {
  test('suma dos números positivos', () => {
    expect(add(3, 4)).toBe(7);
  });

  test('suma números negativos', () => {
    expect(add(-2, -3)).toBe(-5);
  });
});

describe('divide', () => {
  test('divide dos números', () => {
    expect(divide(10, 2)).toBe(5);
  });

  test('lanza error al dividir entre cero', () => {
    expect(() => divide(5, 0)).toThrow('No se puede dividir entre cero.');
  });
});
