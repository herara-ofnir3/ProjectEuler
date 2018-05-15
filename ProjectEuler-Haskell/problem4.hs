digits n 
    | n < 10    = [n]
    | otherwise = (mod n 10):(digits (div n 10))

isPalindromic n = (digits n) == (reverse (digits n))

numbersBy d = [(10 ^ (d - 1))..(10 ^ d - 1)]
numbers = numbersBy 3
products = numbers >>= (\ x -> map (\ y -> x * y) numbers)

problem4 = maximum (filter (\ p -> isPalindromic p) products)
--problem4 = digits 920394020

main = do
    print problem4
