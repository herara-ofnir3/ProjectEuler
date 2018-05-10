isDivisor n m = mod n m == 0

fd n m
    | (div n m) <= m = []
    | isDivisor n m  = [m, div n m] ++ (fd n (m + 1))
    | otherwise      = fd n (m + 1)

divisors n = fd n 2
isPrime n = (divisors n) == []

problem3 = filter (\n -> isPrime n) (divisors 600851475143)

main = do
    print problem3
