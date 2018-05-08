fib n
    | n == 0    = 1
    | n == 1    = 2
    | otherwise = (fibs !! (n - 2)) + (fibs !! (n - 1))

fibs = [fib n | n <- [0..]]

problem2 = sum (filter even (takeWhile (<4000000) fibs))

main = do
    print problem2
