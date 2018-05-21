{-
sieve n [] = []
sieve n (x:xs) 
    | sqrt n <= fromIntegral(x) = x:xs
    | otherwise   = x:(sieve n [ y | y <- xs, y `mod` x /= 0 ])

problem10 = sum(sieve 2000000 [2..2000000])
-}

sieve [] = []
sieve (x:xs) = x:(sieve [ y | y <- xs, y `mod` x /= 0 ])
problem10 = sum (takeWhile (<=10) (sieve (2:[3,5..])))

main = do
    print problem10
