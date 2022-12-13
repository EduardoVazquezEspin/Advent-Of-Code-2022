namespace AdventOfCode
{
    public class Day13
    {
        /*
            let x = document.getElementsByTagName('pre') 
			let y = x[0].textContent 
			let ySplit = y.split('\n') 
			let value = 0
			for(let i =0; i<ySplit.length / 3; i++){
			let first = JSON.parse(ySplit[3*i])
			let second = JSON.parse(ySplit[3*i+1])
			let compare = funComp(first, second)
			if(compare == 0)
			value += i+1
			}
			console.log(value)
			console.log(howManyBelow([[2]]) * (howManyBelow([[6]])+1))


			const funComp = (array1, array2) => {
				for(let k=0; k<Math.min(array1.length, array2.length); k++){
					if(!Array.isArray(array1[k]) && !Array.isArray(array2[k])){
						if(array1[k] < array2[k]) return 0;
						if(array1[k] > array2[k]) return 1;
					}
					else if(Array.isArray(array1[k]) && Array.isArray(array2[k])){
						let res = funComp(array1[k], array2[k])
						if(res != -1) return res;
					}
					else if(!Array.isArray(array1[k])){
						let res = funComp([array1[k]], array2[k])
						if(res != -1) return res;
					}
					else{
						let res = funComp(array1[k], [array2[k]])
						if(res != -1) return res;
					}
				}
				if(array1.length < array2.length) return 0;
				if(array1.length > array2.length) return 1;
				return -1;
			}

			const howManyBelow = (array) => {
			let x = document.getElementsByTagName('pre') 
			let y = x[0].textContent 
			let ySplit = y.split('\n') 
			let value = 1
			for(let i =0; i<ySplit.length / 3; i++){
			let first = JSON.parse(ySplit[3*i])
			let second = JSON.parse(ySplit[3*i+1])
			let compare = funComp(first, array)
			if(compare == 0)
			value++
			compare = funComp(second, array)
			if(compare == 0)
			value++
			}
			return value
			}
         */
    }
}