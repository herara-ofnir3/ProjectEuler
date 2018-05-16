digits n 
    | n < 10    = [n]
    | otherwise = (mod n 10):(digits (div n 10))

isPalindromic n = (digits n) == (reverse (digits n))

d = 3
problem4 = maximum [ p | x <- [(10 ^ (d - 1))..(10 ^ d - 1)], 
                         y <- [x..(10 ^ d - 1)], 
                         let p = x * y, 
                         isPalindromic p ]

main = do
    print problem4
