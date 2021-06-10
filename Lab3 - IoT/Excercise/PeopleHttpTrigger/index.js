const sql = require('mssql');
const parser = require('mssql-connection-string');

class PeopleDbContext {
    constructor(connectionString, log) {
        log("PeopleDbContext object has been created.");
        this.log = log;
        this.config = parser(connectionString);
        this.getPeople = this.getPeople.bind(this);
    }

    async getPeople() {
        this.log("getPeople function - run")
        const connection = await new sql.ConnectionPool(this.config).connect();
        const request = new sql.Request(connection);
        const result = await request.query('select * from People');
        this.log("getPeople function - done")
        return result.recordset;
    }

   /* async addPerson(req){
        var name = req.query.FirstName;
        var surname = req.query.LastName;
        var phonenumber = req.query.PhoneNumber;

        this.log("addPerson function - run")
        const connection = await new sql.ConnectionPool(this.config).connect();
        const request = new sql.Request(connection);
        const result = await request.query('Insert into People values ('+name+', '+surname+', '+phonenumber+')');
        this.log("addPerson function - done")
        return result;
    }

    async deletePerson(req){
        var surname = req.query.LastName;

        this.log("deletePerson function - run")
        const connection = await new sql.ConnectionPool(this.config).connect();
        const request = new sql.Request(connection);
        const result = await request.query('delete from People where LastName = '+surname+'');
        this.log("deletePerson function - done")
        return result;
    }

    async selectPerson(req){
        var surname = req.query.LastName;

        this.log("selectPerson function - run")
        const connection = await new sql.ConnectionPool(this.config).connect();
        const request = new sql.Request(connection);
        const result = await request.query('select * from People where LastName = '+surname+'');
        this.log("selectPerson function - done")
        return result;
    }
    */
}

module.exports = PeopleDbContext;
const common = require('./common');

module.exports = async function (context, req) {
    await common.functionWrapper(context, req, async (body) => {
        const connectionString = process.env['PeopleDb'];
        const peopleDb = new PeopleDbContext(connectionString, context.log);
        body.people = await peopleDb.getPeople();
    });
};