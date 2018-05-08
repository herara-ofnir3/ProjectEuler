isMultipe n m = mod n m == 0
problem1 = sum (filter (\ x -> isMultipe x 3 || isMultipe x 5) [1..999])

main = do
    print problem1
