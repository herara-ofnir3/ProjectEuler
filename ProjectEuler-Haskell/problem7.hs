prime 0 = 2
prime i = head [ x | x <- [(primes !! (i - 1))..], 
                     all (\ p -> (mod x p) /= 0) (take i primes) ]
primes = [ prime i | i <- [0..] ]

problem7 = primes !! (10001 - 1)

main = do
    print problem7
