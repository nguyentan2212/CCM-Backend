const UserManager = artifacts.require("UserManager");

contract("UserManager", (accounts) =>{
    let userManager;

    before(async () => {
        userManager = await UserManager.deployed();
    });

    describe("create new user and retrieving account addresses", async () =>{
        before("create new user with " + accounts[0] , async () =>{
            await userManager.addUser(accounts[3], {from: accounts[0]});
            expectedUser = accounts[3];
        });

        it("can set deactive of a user by its address", async () =>{
            const result = await userManager.setActive(expectedUser, false, { from: accounts[0]});
            assert.ok(result.receipt.status, 'it returns true');
        });

        it("can set admin of a user by its address", async () =>{
            const result = await userManager.setAdmin(expectedUser, true, { from: accounts[0]});
            assert.ok(result.receipt.status, 'it returns true');
        })
    });
});